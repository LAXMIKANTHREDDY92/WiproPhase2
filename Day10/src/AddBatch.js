import React, { useState } from "react";
import { createBatch } from "./batchService";
import { useNavigate } from "react-router-dom";

const AddBatch = () => {
    const [batch, setBatch] = useState({ name: "", startDate: "", seats: 0, createdBy: 1 });
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        await createBatch(batch);
        navigate("/");
    };

    return (
        <div>
            <h2>Add New Batch</h2>
            <form onSubmit={handleSubmit}>
                <input type="text" placeholder="Batch Name" required onChange={(e) => setBatch({ ...batch, name: e.target.value })} />
                <input type="date" required onChange={(e) => setBatch({ ...batch, startDate: e.target.value })} />
                <input type="number" placeholder="Seats" required onChange={(e) => setBatch({ ...batch, seats: parseInt(e.target.value) })} />
                <button type="submit">Add Batch</button>
            </form>
        </div>
    );
};

export default AddBatch;
