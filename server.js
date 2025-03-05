const express = require("express");
const http = require("http");
const { Server } = require("socket.io");
const cors = require("cors");

const app = express();
const server = http.createServer(app);
const io = new Server(server, {
    cors: {
        origin: "*",  // Allow all origins (for testing)
        methods: ["GET", "POST"]
    }
});

app.use(cors());
app.use(express.json());

let tasks = [];  // In-memory storage for tasks

io.on("connection", (socket) => {
    console.log("User connected:", socket.id);

    // Send initial tasks
    socket.emit("loadTasks", tasks);

    // Handle adding a task
    socket.on("addTask", (task) => {
        tasks.push(task);
        io.emit("taskUpdated", tasks); // Broadcast update
        console.log("Task added:", task);
    });

    // Handle deleting a task
    socket.on("deleteTask", (taskId) => {
        tasks = tasks.filter(task => task.id !== taskId);
        io.emit("taskUpdated", tasks);
        console.log("Task deleted:", taskId);
    });

    socket.on("disconnect", () => {
        console.log("User disconnected:", socket.id);
    });
});

server.listen(5000, () => console.log("✅ Server running on port 5000"));
app.get("/", (req, res) => {
  res.send("Server is running!");
});
