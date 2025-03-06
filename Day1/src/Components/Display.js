import React, { useState } from "react";

const Display = ({ name, address }) => {
  const [newName, setNewName] = useState(name);
  const [newAddress, setNewAddress] = useState(address);

  return (
    <div>
      <h3>Modify Details</h3>
      <input
        type="text"
        value={newName}
        onChange={(e) => setNewName(e.target.value)}
      />
      <input
        type="text"
        value={newAddress}
        onChange={(e) => setNewAddress(e.target.value)}
      />
      <p>Updated Name: {newName}</p>
      <p>Updated Address: {newAddress}</p>
    </div>
  );
};

export default Display;
