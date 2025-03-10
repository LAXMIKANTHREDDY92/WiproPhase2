import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./Home";
import AddTask from "./AddTask";
import EditTask from "./EditTask";
import DeleteTask from "./DeleteTask";
import { Provider } from "react-redux";
import store from "./redux/store";

const App = () => (
    <Provider store={store}>
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/add-task" element={<AddTask />} />
                <Route path="/edit-task/:id" element={<EditTask />} />
                <Route path="/delete-task/:id" element={<DeleteTask />} />
            </Routes>
        </Router>
    </Provider>
);

export default App;
