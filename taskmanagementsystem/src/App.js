import React, { useState, useEffect } from "react";
import io from "socket.io-client";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const socket = io("http://localhost:5000");

const App = () => {
    const [tasks, setTasks] = useState([]);
    const [newTask, setNewTask] = useState("");
    const [assignee, setAssignee] = useState("");
    const [isAdmin, setIsAdmin] = useState(false);
    const [username, setUsername] = useState("");

    useEffect(() => {
        socket.on("loadTasks", (loadedTasks) => setTasks(loadedTasks));
        socket.on("taskUpdated", (updatedTasks) => setTasks(updatedTasks));
        socket.on("taskAssigned", (task) => {
            toast.info(`You have been assigned a task: ${task.title}`);
        });

        return () => socket.disconnect();
    }, []);

    const registerUser = () => {
        socket.emit("registerUser", username);
    };

    const addTask = () => {
        if (newTask.trim() !== "" && assignee.trim() !== "") {
            const task = { id: Date.now(), title: newTask, assignee };
            socket.emit("addTask", task);
            setNewTask("");
            setAssignee("");
        }
    };

    const deleteTask = (id) => {
        socket.emit("deleteTask", id);
    };

    const assignTask = (taskId, newAssignee) => {
        socket.emit("assignTask", { taskId, assignee: newAssignee });
    };

    return (
        <div className="p-4">
            <ToastContainer />
            <h1 className="text-xl font-bold mb-4">Task Dashboard (Real-time)</h1>

            {/* Register User */}
            <input
                type="text"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                placeholder="Enter your name"
                className="border p-2 rounded mr-2"
            />
            <button className="bg-green-500 text-white p-2 rounded" onClick={registerUser}>
                Register
            </button>

            {/* Task Form */}
            <div className="mt-4">
                <input
                    type="text"
                    value={newTask}
                    onChange={(e) => setNewTask(e.target.value)}
                    placeholder="Enter task"
                    className="border p-2 rounded mr-2"
                />
                <input
                    type="text"
                    value={assignee}
                    onChange={(e) => setAssignee(e.target.value)}
                    placeholder="Assignee Name"
                    className="border p-2 rounded mr-2"
                />
                <button className="bg-blue-500 text-white p-2 rounded" onClick={addTask}>
                    Add Task
                </button>
            </div>

            {/* Task List */}
            <ul className="mt-4">
                {tasks.map((task) => (
                    <li key={task.id} className="flex justify-between p-2 border-b">
                        <div>
                            <span className="font-bold">{task.title}</span> - Assigned to:{" "}
                            <span className="text-blue-500">{task.assignee}</span>
                        </div>
                        <div>
                            {isAdmin && (
                                <input
                                    type="text"
                                    placeholder="Reassign to..."
                                    onBlur={(e) => assignTask(task.id, e.target.value)}
                                    className="border p-1 rounded mr-2"
                                />
                            )}
                            <button className="text-red-500" onClick={() => deleteTask(task.id)}>
                                X
                            </button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default App;
