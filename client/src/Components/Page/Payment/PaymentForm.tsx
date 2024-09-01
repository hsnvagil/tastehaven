import {
  useStripe,
  useElements,
  PaymentElement,
} from "@stripe/react-stripe-js";
import { useState } from "react";
import { useCreateOrderMutation } from "../../../Apis/orderApi";
import { toastNotify } from "../../../Helper";
import { apiResponse, cartItemModel } from "../../../Interfaces";
import { SD_Status } from "../../../Utility/SD";
import { orderSummaryProps } from "../Order/orderSummaryProps";
import { useNavigate } from "react-router";

const PaymentForm = ({ data, userInput }: orderSummaryProps) => {
  const navigate = useNavigate();
  const stripe = useStripe();
  const elements = useElements();
  const [createOrder] = useCreateOrderMutation();
  const [isProcessing, setIsProcessing] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (!stripe || !elements) {
      return;
    }
    setIsProcessing(true);
    const result = await stripe.confirmPayment({
      elements,
      confirmParams: {
        return_url: "https://example.com/order/123/complete",
      },
      redirect: "if_required",
    });

    if (result.error) {
      toastNotify("An unexpected error occurred.", "error");
      setIsProcessing(false);
    } else {
      let grandTotal = 0;
      let totalItems = 0;
      const orderDetailsDTO: any = [];

      data.cartItems?.forEach((item: cartItemModel) => {
        const tempOrderDetail: any = {
          menuItemId: item.menuItem?.id,
          quantity: item.quantity,
          itemName: item.menuItem?.name,
          price: item.menuItem?.price,
        };
        orderDetailsDTO.push(tempOrderDetail);
        grandTotal += item.quantity! * item.menuItem?.price!;
        totalItems += item.quantity!;
      });

      const response: apiResponse = await createOrder({
        pickupName: userInput.name,
        pickupPhoneNumber: userInput.phoneNumber,
        pickupEmail: userInput.email,
        totalItems,
        orderTotal: grandTotal,
        orderDetails: orderDetailsDTO,
        stripePaymentIntentID: data.stripePaymentIntentId,
        applicationUserId: data.userId,
        status:
            result.paymentIntent.status === "succeeded"
                ? SD_Status.CONFIRMED
                : SD_Status.PENDING,
      });

      if (response) {
        if (response.data?.result.status === SD_Status.CONFIRMED) {
          navigate(
              `/order/orderConfirmed/${response.data.result.orderHeaderId}`
          );
        } else {
          navigate("/failed");
        }
      }
    }
    setIsProcessing(false);
  };

  return (
      <form onSubmit={handleSubmit} className="payment-form">
        <PaymentElement />
        <button
            disabled={!stripe || isProcessing}
            className="btn btn-primary mt-4 w-100"
        >
        <span id="button-text">
          {isProcessing ? "Processing..." : "Submit Order"}
        </span>
        </button>
      </form>
  );
};

export default PaymentForm;
