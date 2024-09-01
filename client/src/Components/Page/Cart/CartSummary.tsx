import React from "react";
import { useSelector, useDispatch } from "react-redux";
import { cartItemModel, userModel } from "../../../Interfaces";
import {
  removeFromCart,
  updateQuantity,
} from "../../../Storage/Redux/shoppingCartSlice";
import { RootState } from "../../../Storage/Redux/store";
import { useUpdateShoppingCartMutation } from "../../../Apis/shoppingCartApi";

function CartSummary() {
  const dispatch = useDispatch();
  const [updateShoppingCart] = useUpdateShoppingCartMutation();
  const shoppingCartFromStore: cartItemModel[] = useSelector(
      (state: RootState) => state.shoppingCartStore.cartItems ?? []
  );
  const userData: userModel = useSelector(
      (state: RootState) => state.userAuthStore
  );

  if (shoppingCartFromStore.length === 0) {
    return (
        <div className="p-5 text-center">
          <h5>Your cart is empty.</h5>
          <p>Please add items to continue.</p>
        </div>
    );
  }

  const handleQuantity = (
      updateQuantityBy: number,
      cartItem: cartItemModel
  ) => {
    if (updateQuantityBy === -1 && cartItem.quantity === 1) {
      updateShoppingCart({
        menuItemId: cartItem.menuItem?.id,
        updateQuantityBy: 0,
        userId: userData.id,
      });
      dispatch(removeFromCart({ cartItem, quantity: 0 }));
    } else {
      updateShoppingCart({
        menuItemId: cartItem.menuItem?.id,
        updateQuantityBy: updateQuantityBy,
        userId: userData.id,
      });
      dispatch(
          updateQuantity({
            cartItem,
            quantity: cartItem.quantity! + updateQuantityBy,
          })
      );
    }
  };

  return (
      <div className="container p-4">
        <h4 className="text-center text-primary mb-4">Cart Summary</h4>

        {shoppingCartFromStore.map((cartItem: cartItemModel, index: number) => (
            <div
                key={index}
                className="d-flex flex-sm-row flex-column align-items-center custom-card-shadow rounded mb-3 p-3 bg-white"
            >
              <div className="p-3">
                <img
                    src={cartItem.menuItem?.image}
                    alt={cartItem.menuItem?.name}
                    width={"100px"}
                    className="rounded-circle"
                />
              </div>

              <div className="p-2 mx-3 w-100">
                <div className="d-flex justify-content-between align-items-center mb-2">
                  <h4 className="mb-0" style={{ fontWeight: 500 }}>
                    {cartItem.menuItem?.name}
                  </h4>
                  <h4 className="mb-0 text-primary">
                    ${(cartItem.quantity! * cartItem.menuItem!.price).toFixed(2)}
                  </h4>
                </div>
                <h6 className="text-muted">${cartItem.menuItem!.price} each</h6>

                <div className="d-flex justify-content-between align-items-center mt-3">
                  <div className="d-flex align-items-center">
                    <button
                        className="btn btn-outline-secondary btn-sm"
                        onClick={() => handleQuantity(-1, cartItem)}
                    >
                      <i className="bi bi-dash"></i>
                    </button>
                    <span className="mx-3 h5 mb-0">{cartItem.quantity}</span>
                    <button
                        className="btn btn-outline-secondary btn-sm"
                        onClick={() => handleQuantity(1, cartItem)}
                    >
                      <i className="bi bi-plus"></i>
                    </button>
                  </div>

                  <button
                      className="btn btn-outline-danger btn-sm"
                      onClick={() => handleQuantity(0, cartItem)}
                  >
                    <i className="bi bi-trash-fill"></i> Remove
                  </button>
                </div>
              </div>
            </div>
        ))}
      </div>
  );
}

export default CartSummary;
