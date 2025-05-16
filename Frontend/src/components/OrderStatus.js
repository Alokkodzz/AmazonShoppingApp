import React from "react";

export default function OrderStatus({ message }) {
  return (
    <div style={{
      padding: "1rem",
      marginBottom: "1rem",
      border: "1px solid green",
      backgroundColor: "#d4edda",
      color: "#155724"
    }}>
      {message}
    </div>
  );
}
