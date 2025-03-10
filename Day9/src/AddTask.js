import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { addTask } from "./redux/taskSlice";
import { useNavigate } from "react-router-dom";
import { v4 as uuidv4 } from "uuid";

const AddTask = () => {
    const [task, setTask] = useState({ name: "", description: "", date: "", createdBy: "" });
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();
        dispatch(addTask({ id: uuidv4(), ...task }));
        navigate("/");
    };

    return (
        <div>
            <h2>Add New Task</h2>
            <form onSubmit={handleSubmit}>
                <input type="text" placeholder="Task Name" onChange={(e) => setTask({ ...task, name: e.target.value })} required />
                <input type="text" placeholder="Description" onChange={(e) => setTask({ ...task, description: e.target.value })} required />
                <input type="date" onChange={(e) => setTask({ ...task, date: e.target.value })} required />
                <input type="text" placeholder="Created By" onChange={(e) => setTask({ ...task, createdBy: e.target.value })} required />
                <button type="submit">Add Task</button>
            </form>
        </div>
    );
};

export default AddTask;
