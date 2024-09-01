import React from "react";
import { getStatusColor } from "../../../Helper";
import { cartItemModel } from "../../../Interfaces";
import { orderSummaryProps } from "./orderSummaryProps";
import { useNavigate } from "react-router-dom";
import { SD_Roles, SD_Status } from "../../../Utility/SD";
import { RootState } from "../../../Storage/Redux/store";
import { useSelector } from "react-redux";
import { useState } from "react";
import { useUpdateOrderHeaderMutation } from "../../../Apis/orderApi";
import { MainLoader } from "../Common";

function OrderSummary({ data, userInput }: orderSummaryProps) {
  const badgeTypeColor = getStatusColor(data.status!);
  const navigate = useNavigate();
  const userData = useSelector((state: RootState) => state.userAuthStore);
  const [loading, setIsLoading] = useState(false);
  const [updateOrderHeader] = useUpdateOrderHeaderMutation();
  const nextStatus: any =
      data.status! === SD_Status.CONFIRMED
          ? { color: "info", value: SD_Status.BEING_COOKED }
          : data.status! === SD_Status.BEING_COOKED
              ? { color: "warning", value: SD_Status.READY_FOR_PICKUP }
              : data.status! === SD_Status.READY_FOR_PICKUP && {
            color: "success",
            value: SD_Status.COMPLETED,
          };

  const handleNextStatus = async () => {
    setIsLoading(true);
    await updateOrderHeader({
      orderHeaderId: data.id,
      status: nextStatus.value,
    });

    setIsLoading(false);
  };

  const handleCancel = async () => {
    setIsLoading(true);
    await updateOrderHeader({
      orderHeaderId: data.id,
      status: SD_Status.CANCELLED,
    });
    setIsLoading(false);
  };

  return (
      <div className="order-summary-container">
        {loading && <MainLoader />}
        {!loading && (
            <>
              <div className="order-summary-header">
                <h3 className="order-summary-title">Order Summary</h3>
                <span className={`status-badge bg-${badgeTypeColor}`}>
              {data.status}
            </span>
              </div>
              <div className="order-summary-details mt-4">
                <div className="detail-item">
                  <strong>Name:</strong> {userInput.name}
                </div>
                <div className="detail-item">
                  <strong>Email:</strong> {userInput.email}
                </div>
                <div className="detail-item">
                  <strong>Phone:</strong> {userInput.phoneNumber}
                </div>
                <div className="detail-item">
                  <h4 className="menu-items-title">Menu Items</h4>
                  <div className="menu-items-list mt-3">
                    {data.cartItems?.map(
                        (cartItem: cartItemModel, index: number) => {
                          return (
                              <div className="menu-item" key={index}>
                                <p>{cartItem.menuItem?.name}</p>
                                <p>
                                  ${cartItem.menuItem?.price} x {cartItem.quantity} = $
                                  {(cartItem.menuItem?.price ?? 0) *
                                      (cartItem.quantity ?? 0)}
                                </p>
                              </div>
                          );
                        }
                    )}
                  </div>
                  <hr />
                  <h4 className="total-price">
                    Total: ${data.cartTotal?.toFixed(2)}
                  </h4>
                </div>
              </div>
              <div className="order-summary-actions mt-4">
                <button className="btn btn-secondary" onClick={() => navigate(-1)}>
                  Back to Orders
                </button>
                {userData.role === SD_Roles.ADMIN && (
                    <>
                      {nextStatus.value !== undefined && <button
                          className={`btn btn-${nextStatus.color}`}
                          onClick={handleNextStatus}
                      >
                        Next Status: {nextStatus.value}
                      </button>}
                      {data.status !== "Cancelled" &&
                          <button className="btn btn-danger" onClick={handleCancel}>
                            Cancel Order
                          </button>
                      }
                      </>
                )}
              </div>
            </>
        )}
      </div>
  );
}

export default OrderSummary;
