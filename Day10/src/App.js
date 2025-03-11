import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./Home";  // ✅ Correct path
import AddBatch from "./AddBatch";  // ✅ Correct path
import EditBatch from "./EditBatch"; 


const App = () => (
    <Router>
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/add-batch" element={<AddBatch />} />
            <Route path="/edit-batch/:id" element={<EditBatch />} />
        </Routes>
    </Router>
);

export default App;
