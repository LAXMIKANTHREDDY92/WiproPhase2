import React, { useEffect, useState } from "react";
import { getBatches, deleteBatch } from "./batchService";  // ✅ Correct import
import { Link } from "react-router-dom";
import "./Home.css";  // ✅ Import CSS for styling
const Home = () => {
    const [batches, setBatches] = useState([]);

    useEffect(() => {
        fetchBatches();
    }, []);

    const fetchBatches = async () => {
        try {
            const response = await getBatches();
            setBatches(response.data);
        } catch (error) {
            console.error("Error fetching batches:", error);
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm("Are you sure you want to delete this batch?")) {
            await deleteBatch(id);
            fetchBatches();
        }
    };

    return (
        <div>
            <h2>Batch List</h2>
            <Link to="/add-batch">Add New Batch</Link>
            <table border="1">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Start Date</th>
                        <th>Seats</th>
                        <th>Created By</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {batches.map((batch) => (
                        <tr key={batch.batchId}>
                            <td>{batch.batchId}</td>
                            <td>{batch.name}</td>
                            <td>{batch.startDate.split("T")[0]}</td>
                            <td>{batch.seats}</td>
                            <td>{batch.createdBy}</td>
                            <td>
                                <Link to={`/edit-batch/${batch.batchId}`}>Edit</Link> |
                                <button onClick={() => handleDelete(batch.batchId)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default Home;
