import React, { useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { useGetMenuItemByIdQuery } from "../Apis/menuItemApi";
import { useUpdateShoppingCartMutation } from "../Apis/shoppingCartApi";
import { MainLoader, MiniLoader } from "../Components/Page/Common";
import { apiResponse, userModel } from "../Interfaces";
import { toastNotify } from "../Helper";
import { RootState } from "../Storage/Redux/store";
import { useSelector } from "react-redux";

function MenuItemDetails() {
  const { menuItemId } = useParams();
  const { data, isLoading } = useGetMenuItemByIdQuery(menuItemId);
  const navigate = useNavigate();
  const [quantity, setQuantity] = useState(1);
  const [isAddingToCart, setIsAddingToCart] = useState<boolean>(false);
  const [updateShoppingCart] = useUpdateShoppingCartMutation();
  const userData: userModel = useSelector(
      (state: RootState) => state.userAuthStore
  );

  const handleQuantity = (counter: number) => {
    setQuantity((prevQuantity) => Math.max(prevQuantity + counter, 1));
  };

  const handleAddToCart = async (menuItemId: number) => {
    if (!userData.id) {
      navigate("/login");
      return;
    }
    setIsAddingToCart(true);
    const response: apiResponse = await updateShoppingCart({
      menuItemId: menuItemId,
      updateQuantityBy: quantity,
      userId: userData.id,
    });

    if (response.data && response.data.isSuccess) {
      toastNotify("Item added to cart successfully!");
    }
    setIsAddingToCart(false);
  };

  return (
      <div className="container pt-4 pt-md-5">
        {!isLoading ? (
            <div className="row">
              <div className="col-lg-7">
                <h2 className="text-success mb-3">{data.result?.name}</h2>
                <div className="d-flex mb-3">
                  <span className="badge bg-dark me-2">{data.result?.category}</span>
                  {data.result?.specialTag && (
                      <span className="badge bg-light text-dark">
                  {data.result?.specialTag}
                </span>
                  )}
                </div>
                <p className="fs-5 mb-4">{data.result?.description}</p>
                <div className="d-flex align-items-center mb-4">
                  <span className="h3 me-3">${data.result?.price}</span>
                  <div className="d-flex align-items-center">
                    <button
                        className="btn btn-outline-secondary"
                        onClick={() => handleQuantity(-1)}
                    >
                      <i className="bi bi-dash"></i>
                    </button>
                    <span className="mx-3 h4 mb-0">{quantity}</span>
                    <button
                        className="btn btn-outline-secondary"
                        onClick={() => handleQuantity(1)}
                    >
                      <i className="bi bi-plus"></i>
                    </button>
                  </div>
                </div>
                <div className="row">
                  <div className="col-md-6 mb-2">
                    <button
                        className="btn btn-primary w-100"
                        onClick={() => handleAddToCart(data.result?.id)}
                        disabled={isAddingToCart}
                    >
                      {isAddingToCart ? <MiniLoader /> : "Add to Cart"}
                    </button>
                  </div>
                  <div className="col-md-6">
                    <button
                        className="btn btn-outline-secondary w-100"
                        onClick={() => navigate(-1)}
                    >
                      Back to Home
                    </button>
                  </div>
                </div>
              </div>
              <div className="col-lg-5 text-center mt-4 mt-lg-0">
                <img
                    src={data.result?.image}
                    className="img-fluid rounded"
                    alt={data.result?.name}
                />
              </div>
            </div>
        ) : (
            <div className="d-flex justify-content-center">
              <MainLoader />
            </div>
        )}
      </div>
  );
}

export default MenuItemDetails;
