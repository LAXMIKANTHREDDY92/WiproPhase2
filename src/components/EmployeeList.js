import React, { useEffect, useState } from "react";
import axios from "axios";

const EmployeeList = ({ onEdit }) => {
  const [employees, setEmployees] = useState([]);

  useEffect(() => {
    axios.get("http://localhost:5000/employees")
      .then(response => setEmployees(response.data))
      .catch(error => console.error("Error fetching employees:", error));
  }, []);

  return (
    <div>
      <h2>Employee List</h2>
      <ul className="list-group">
        {employees.map(emp => (
          <li key={emp.id} className="list-group-item d-flex justify-content-between">
            {emp.name} - {emp.dept} (Manager: {emp.manager})
            <button className="btn btn-warning btn-sm" onClick={() => onEdit(emp)}>Edit</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default EmployeeList;
