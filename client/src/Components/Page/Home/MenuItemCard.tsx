import React, { useState } from "react";
import { apiResponse, menuItemModel, userModel } from "../../../Interfaces";
import { Link, useNavigate } from "react-router-dom";
import { useUpdateShoppingCartMutation } from "../../../Apis/shoppingCartApi";
import { MiniLoader } from "../Common";
import { toastNotify } from "../../../Helper";
import { RootState } from "../../../Storage/Redux/store";
import { useSelector } from "react-redux";

interface Props {
  menuItem: menuItemModel;
}

function MenuItemCard({ menuItem }: Props) {
  const navigate = useNavigate();
  const [isAddingToCart, setIsAddingToCart] = useState<boolean>(false);
  const [updateShoppingCart] = useUpdateShoppingCartMutation();
  const userData: userModel = useSelector(
      (state: RootState) => state.userAuthStore
  );

  const handleAddToCart = async (menuItemId: number) => {
    if (!userData.id) {
      navigate("/login");
      return;
    }
    setIsAddingToCart(true);

    const response: apiResponse = await updateShoppingCart({
      menuItemId: menuItemId,
      updateQuantityBy: 1,
      userId: userData.id,
    });
    if (response.data && response.data.isSuccess) {
      toastNotify("Item added to cart successfully!");
    }
    setIsAddingToCart(false);
  };

  return (
      <div className="col-md-4 col-12 p-4">
        <div
            className="card shadow-sm"
            style={{ borderRadius: "15px", overflow: "hidden" }}
        >
          <div className="card-body pt-2 position-relative">
            <Link to={`/menu-item-details/${menuItem.id}`} className="d-block text-center">
              <img
                  src={menuItem.image}
                  alt={menuItem.name}
                  className="img-fluid mt-4"
                  style={{ borderRadius: "50%", width: "155px", height: "125px", objectFit: "cover" }}
              />
            </Link>

            {menuItem.specialTag && (
                <span
                    className="badge bg-success position-absolute"
                    style={{
                      top: "10px",
                      left: "10px",
                      padding: "5px 10px",
                      borderRadius: "5px",
                    }}
                >
              {menuItem.specialTag}
            </span>
            )}

            {isAddingToCart ? (
                <div
                    className="position-absolute"
                    style={{
                      top: "10px",
                      right: "10px",
                    }}
                >
                  <MiniLoader />
                </div>
            ) : (
                <i
                    className="bi bi-cart-plus text-danger position-absolute"
                    style={{
                      top: "10px",
                      right: "10px",
                      cursor: "pointer",
                      fontSize: "1.5rem",
                    }}
                    onClick={() => handleAddToCart(menuItem.id)}
                ></i>
            )}

            <div className="text-center mt-3">
              <h5 className="card-title text-success">
                <Link
                    to={`/menu-item-details/${menuItem.id}`}
                    style={{ textDecoration: "none", color: "green" }}
                >
                  {menuItem.name}
                </Link>
              </h5>
              <span className="badge bg-secondary mb-2">
              {menuItem.category}
            </span>
              <p className="card-text" style={{ fontSize: "0.9rem", height:"20px", overflow:"hidden" }}>
                {menuItem.description}
              </p>
              <div className="text-center">
                <h4 className="text-primary">${menuItem.price.toFixed(2)}</h4>
              </div>
            </div>
          </div>
        </div>
      </div>
  );
}

export default MenuItemCard;
