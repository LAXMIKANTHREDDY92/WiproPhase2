import React, { useEffect, useState } from "react";
import { getBatchById, updateBatch } from "./batchService";  // ✅ Correct import
import { useNavigate, useParams } from "react-router-dom";

const EditBatch = () => {
    const { id } = useParams();
    const [batch, setBatch] = useState({ name: "", startDate: "", seats: 0, createdBy: 1 });
    const navigate = useNavigate();

    useEffect(() => {
        fetchBatch();
    }, []);

    const fetchBatch = async () => {
        const response = await getBatchById(id);
        setBatch(response.data);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        await updateBatch(id, batch);
        navigate("/");
    };

    return (
        <div>
            <h2>Edit Batch</h2>
            <form onSubmit={handleSubmit}>
                <input type="text" value={batch.name} onChange={(e) => setBatch({ ...batch, name: e.target.value })} required />
                <input type="date" value={batch.startDate.split("T")[0]} onChange={(e) => setBatch({ ...batch, startDate: e.target.value })} required />
                <input type="number" value={batch.seats} onChange={(e) => setBatch({ ...batch, seats: parseInt(e.target.value) })} required />
                <button type="submit">Update Batch</button>
            </form>
        </div>
    );
};

export default EditBatch;
