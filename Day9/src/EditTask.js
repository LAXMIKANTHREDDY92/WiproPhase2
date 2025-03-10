import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { editTask } from "./redux/taskSlice";
import { useNavigate, useParams } from "react-router-dom";

const EditTask = () => {
    const { id } = useParams();
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const task = useSelector(state => state.tasks.tasks.find(task => task.id === id));

    const [updatedTask, setUpdatedTask] = useState(task);

    const handleSubmit = (e) => {
        e.preventDefault();
        dispatch(editTask(updatedTask));
        navigate("/");
    };

    return (
        <div>
            <h2>Edit Task</h2>
            <form onSubmit={handleSubmit}>
                <input type="text" value={updatedTask.name} onChange={(e) => setUpdatedTask({ ...updatedTask, name: e.target.value })} required />
                <input type="text" value={updatedTask.description} onChange={(e) => setUpdatedTask({ ...updatedTask, description: e.target.value })} required />
                <input type="date" value={updatedTask.date} onChange={(e) => setUpdatedTask({ ...updatedTask, date: e.target.value })} required />
                <input type="text" value={updatedTask.createdBy} onChange={(e) => setUpdatedTask({ ...updatedTask, createdBy: e.target.value })} required />
                <button type="submit">Update Task</button>
            </form>
        </div>
    );
};

export default EditTask;
