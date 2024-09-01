import React, { useState, useEffect } from "react";
import { useSelector } from "react-redux";
import { useInitiatePaymentMutation } from "../../../Apis/paymentApi";
import { inputHelper } from "../../../Helper";
import { apiResponse, cartItemModel } from "../../../Interfaces";
import { RootState } from "../../../Storage/Redux/store";
import { MiniLoader } from "../Common";
import { useNavigate } from "react-router";

export default function CartPickUpDetails() {
  const [loading, setLoading] = useState(false);
  const shoppingCartFromStore: cartItemModel[] = useSelector(
      (state: RootState) => state.shoppingCartStore.cartItems ?? []
  );
  const userData = useSelector((state: RootState) => state.userAuthStore);

  const [userInput, setUserInput] = useState({
    name: userData.fullName,
    email: userData.email,
    phoneNumber: "",
  });

  const navigate = useNavigate();
  const [initiatePayment] = useInitiatePaymentMutation();

  let grandTotal = shoppingCartFromStore.reduce(
      (acc, cartItem) =>
          acc + (cartItem.menuItem?.price ?? 0) * (cartItem.quantity ?? 0),
      0
  );

  let totalItems = shoppingCartFromStore.reduce(
      (acc, cartItem) => acc + (cartItem.quantity ?? 0),
      0
  );

  const handleUserInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    const tempData = inputHelper(e, userInput);
    setUserInput(tempData);
  };

  useEffect(() => {
    setUserInput({
      name: userData.fullName,
      email: userData.email,
      phoneNumber: "",
    });
  }, [userData]);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoading(true);

    const { data }: apiResponse = await initiatePayment(userData.id);

    navigate("/payment", {
      state: { apiResult: data?.result, userInput },
    });
  };

  return (
      <div className="pickup-details-container border rounded p-4 bg-white shadow-sm">
        <h2 className="text-center text-primary mb-4">Pickup Details</h2>
        <hr />
        <form onSubmit={handleSubmit} className="col-12 col-md-8 mx-auto">
          <div className="form-group mt-3">
            <label htmlFor="name">Pickup Name</label>
            <input
                type="text"
                value={userInput.name}
                className="form-control"
                placeholder="Enter your name"
                name="name"
                onChange={handleUserInput}
                required
            />
          </div>
          <div className="form-group mt-3">
            <label htmlFor="email">Pickup Email</label>
            <input
                type="email"
                value={userInput.email}
                className="form-control"
                placeholder="Enter your email"
                name="email"
                onChange={handleUserInput}
                required
            />
          </div>
          <div className="form-group mt-3">
            <label htmlFor="phoneNumber">Pickup Phone Number</label>
            <input
                type="text"
                value={userInput.phoneNumber}
                className="form-control"
                placeholder="Enter your phone number"
                name="phoneNumber"
                onChange={handleUserInput}
                required
            />
          </div>
          <div className="order-summary card p-3 bg-light mt-4">
            <h5>Grand Total: ${grandTotal.toFixed(2)}</h5>
            <h5>No. of items: {totalItems}</h5>
          </div>
          <button
              type="submit"
              className="btn btn-primary btn-lg mt-4 w-100"
              disabled={loading || shoppingCartFromStore.length === 0}
          >
            {loading ? <MiniLoader /> : "Place Order"}
          </button>
        </form>
      </div>
  );
}
