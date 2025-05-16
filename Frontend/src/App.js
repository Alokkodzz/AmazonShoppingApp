import React, { useState } from "react";
import ProductList from "./components/ProductList";
import OrderStatus from "./components/OrderStatus";

function App() {
  const [orderStatus, setOrderStatus] = useState("");

  const handleOrderPlaced = (message) => {
    setOrderStatus(message);
    setTimeout(() => setOrderStatus(""), 3000);
  };

  return (
    <div style={{ margin: "2rem" }}>
      <h1>Amazon Shopping App</h1>
      {orderStatus && <OrderStatus message={orderStatus} />}
      <ProductList onOrderPlaced={handleOrderPlaced} />
    </div>
  );
}

export default App;
