import React, { useEffect, useState } from "react";
import axios from "axios";

const PRODUCT_SERVICE_URL = process.env.REACT_APP_PRODUCT_SERVICE_URL || "http://localhost:5001";
const ORDER_SERVICE_URL = process.env.REACT_APP_ORDER_SERVICE_URL || "http://localhost:5003";

export default function ProductList({ onOrderPlaced }) {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    axios.get(`${PRODUCT_SERVICE_URL}/api/products`)
      .then(response => {
        setProducts(response.data);
        setLoading(false);
      })
      .catch(() => {
        setProducts([]);
        setLoading(false);
      });
  }, []);

  const placeOrder = (productId) => {
    const order = { userId: 1, productId, quantity: 1 };
    axios.post(`${ORDER_SERVICE_URL}/api/orders`, order)
      .then(() => onOrderPlaced("Order placed successfully!"))
      .catch(() => onOrderPlaced("Failed to place order."));
  };

  if (loading) return <p>Loading products...</p>;

  return (
    <div>
      <ul>
        {products.length === 0 && <p>No products available.</p>}
        {products.map(product => (
          <li key={product.id} style={{ marginBottom: "1rem" }}>
            <b>{product.name}</b> - ${product.price.toFixed(2)}<br />
            <button onClick={() => placeOrder(product.id)}>Order</button>
          </li>
        ))}
      </ul>
    </div>
  );
}
