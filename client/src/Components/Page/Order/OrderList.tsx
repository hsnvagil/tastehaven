import React from "react";
import { orderHeaderModel } from "../../../Interfaces";
import { MainLoader } from "../Common";
import OrderListProps from "./orderListType";
import { useNavigate } from "react-router-dom";
import { getStatusColor } from "../../../Helper";

function OrderList({ isLoading, orderData }: OrderListProps) {
    const navigate = useNavigate();

    return (
        <>
            {isLoading && <MainLoader />}
            {!isLoading && (
                <div className="order-list-container px-4 py-4">
                    <table className="table table-hover align-middle">
                        <thead className="table-light">
                        <tr>
                            <th scope="col">ID</th>
                            <th scope="col">Name</th>
                            <th scope="col">Phone</th>
                            <th scope="col">Total</th>
                            <th scope="col">Items</th>
                            <th scope="col">Date</th>
                            <th scope="col">Status</th>
                            <th scope="col">Details</th>
                        </tr>
                        </thead>
                        <tbody>
                        {orderData.map((orderItem: orderHeaderModel) => {
                            const badgeColor = getStatusColor(orderItem.status!);
                            return (
                                <tr key={orderItem.orderHeaderId}>
                                    <td>{orderItem.orderHeaderId}</td>
                                    <td>{orderItem.pickupName}</td>
                                    <td>{orderItem.pickupPhoneNumber}</td>
                                    <td>${orderItem.orderTotal!.toFixed(2)}</td>
                                    <td>{orderItem.totalItems}</td>
                                    <td>
                                        {new Date(orderItem.orderDate!).toLocaleDateString()}
                                    </td>
                                    <td>
                      <span className={`badge bg-${badgeColor}`}>
                        {orderItem.status}
                      </span>
                                    </td>
                                    <td>
                                        <button
                                            className="btn btn-outline-primary btn-sm"
                                            onClick={() =>
                                                navigate(
                                                    "/order/order-details/" +
                                                    orderItem.orderHeaderId
                                                )
                                            }
                                        >
                                            Details
                                        </button>
                                    </td>
                                </tr>
                            );
                        })}
                        </tbody>
                    </table>
                </div>
            )}
        </>
    );
}

export default OrderList;
