import React, { useState } from "react";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import EmployeeList from "./components/EmployeeList";
import EmployeeForm from "./components/EmployeeForm";
import axios from "axios";

const App = () => {
  const [selectedEmployee, setSelectedEmployee] = useState(null);

  const handleEdit = (employee) => setSelectedEmployee(employee);

  const handleSubmit = (values, { resetForm }) => {
    if (selectedEmployee) {
      axios.put(`http://localhost:5000/employees/${selectedEmployee.id}`, values)
        .then(() => {
          setSelectedEmployee(null);
          resetForm();
        })
        .catch(err => console.error("Error updating employee", err));
    } else {
      axios.post("http://localhost:5000/employees", values)
        .then(() => resetForm())
        .catch(err => console.error("Error adding employee", err));
    }
  };

  return (
    <Router>
      <div className="container mt-5">
        <h1 className="text-center">Employee Management</h1>
        <nav className="mb-3">
          <Link to="/" className="btn btn-primary me-2">Employee List</Link>
          <Link to="/add" className="btn btn-success">Add Employee</Link>
        </nav>
        <Routes>
          <Route path="/" element={<EmployeeList onEdit={handleEdit} />} />
          <Route path="/add" element={<EmployeeForm initialValues={selectedEmployee || { name: "", address: "", dept: "", manager: "" }} onSubmit={handleSubmit} />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;
