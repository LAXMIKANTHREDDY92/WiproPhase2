import React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";

const Home = () => {
    const tasks = useSelector(state => state.tasks.tasks);

    return (
        <div>
            <h2>Task List</h2>
            <Link to="/add-task">Add New Task</Link>
            <table border="1">
                <thead>
                    <tr>
                        <th>Task ID</th>
                        <th>Task Name</th>
                        <th>Description</th>
                        <th>Date</th>
                        <th>Created By</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {tasks.map(task => (
                        <tr key={task.id}>
                            <td>{task.id}</td>
                            <td>{task.name}</td>
                            <td>{task.description}</td>
                            <td>{task.date}</td>
                            <td>{task.createdBy}</td>
                            <td>
                                <Link to={`/edit-task/${task.id}`}>Edit</Link> |
                                <Link to={`/delete-task/${task.id}`}>Delete</Link>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default Home;
