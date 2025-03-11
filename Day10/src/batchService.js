import axios from "axios";

const API_URL = "https://localhost:7184/api/batch";  // ✅ Ensure correct API URL

// ✅ Fetch all batches
export const getBatches = async () => await axios.get(API_URL);

// ✅ Fetch a batch by ID
export const getBatchById = async (id) => await axios.get(`${API_URL}/${id}`);

// ✅ Create a new batch
export const createBatch = async (batch) => await axios.post(API_URL, batch, {
    headers: { "Content-Type": "application/json" }
});

// ✅ Update an existing batch
export const updateBatch = async (id, batch) => await axios.put(`${API_URL}/${id}`, batch, {
    headers: { "Content-Type": "application/json" }
});

// ✅ Delete a batch
export const deleteBatch = async (id) => await axios.delete(`${API_URL}/${id}`);
