import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './BatchList.css';

const BatchList = () => {
  const [batches, setBatches] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    fetchBatches();
  }, []);

  // ✅ Fetch batches from API
  const fetchBatches = async () => {
    const token = localStorage.getItem('token');

    try {
      const response = await axios.get('https://localhost:7195/api/Batch', {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      console.log(response.data);
      setBatches(response.data);
    } catch (error) {
      console.error('Error fetching batches:', error);
      alert('Failed to fetch batches. Please try again later.');
    }
  };

  // ✅ Delete batch by ID
  const handleDelete = async (batchId) => {
    const confirmDelete = window.confirm('Are you sure you want to delete this batch?');
    if (!confirmDelete) return;

    try {
      const token = localStorage.getItem('token');
      await axios.delete(`https://localhost:7153/api/Batch/${batchId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      alert('Batch deleted successfully!');
      setBatches((prevBatches) => prevBatches.filter((batch) => batch.batchId !== batchId)); // Remove from UI
    } catch (error) {
      console.error('Error deleting batch:', error);
      alert('Failed to delete batch.');
    }
  };

  // ✅ Handle logout and navigate to login page
  const handleLogout = () => {
    localStorage.removeItem('token');
    navigate('/');
  };

  return (
    <div className="batch-list-container">
      <h2>Batch List</h2>

      {/* Navigation Buttons */}
      <div className="button-group">
        <button onClick={() => navigate('/add')}>Add Batch</button>
        <button onClick={handleLogout}>Logout</button>
      </div>

      {/* Display Batches */}
      {batches.length === 0 ? (
        <p>No batches found.</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Start Date</th>
              <th>Seats</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {batches.map((batch) => (
              <tr key={batch.batchId}>
                <td>{batch.batchId}</td>
                <td>{batch.name}</td>
                <td>{new Date(batch.startDate).toLocaleDateString()}</td>
                <td>{batch.seats}</td>

                {/* Edit & Delete Buttons */}
                <td>
                  <button
                    className="edit-button"
                    onClick={() => navigate(`/edit/${batch.batchId}`)}
                  >
                    Edit
                  </button>

                  <button
                    className="delete-button"
                    onClick={() => handleDelete(batch.batchId)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default BatchList;
