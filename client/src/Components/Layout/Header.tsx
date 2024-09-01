import React from "react";
import { NavLink, useNavigate } from "react-router-dom";
import { cartItemModel, userModel } from "../../Interfaces";
import { useSelector, useDispatch } from "react-redux";
import { RootState } from "../../Storage/Redux/store";
import {
  emptyUserState,
  setLoggedInUser,
} from "../../Storage/Redux/userAuthSlice";
import { SD_Roles } from "../../Utility/SD";
let logo = require("../../Assets/Images/tastehaven.jpg");

function Header() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const shoppingCartFromStore: cartItemModel[] = useSelector(
      (state: RootState) => state.shoppingCartStore.cartItems ?? []
  );

  const userData: userModel = useSelector(
      (state: RootState) => state.userAuthStore
  );

  const handleLogout = () => {
    localStorage.removeItem("token");
    dispatch(setLoggedInUser({ ...emptyUserState }));
    navigate("/");
  };

  return (
      <nav className="navbar navbar-expand-lg bg-dark navbar-dark">
        <div className="container-fluid">
          <NavLink className="navbar-brand" to="/">
            <img src={logo} alt="Logo" style={{ height: "40px" }} className="m-1" />
          </NavLink>
          <button
              className="navbar-toggler"
              type="button"
              data-bs-toggle="collapse"
              data-bs-target="#navbarNav"
              aria-controls="navbarNav"
              aria-expanded="false"
              aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
              <li className="nav-item">
                <NavLink className="nav-link" to="/">
                  Home
                </NavLink>
              </li>
              {userData.role === SD_Roles.ADMIN ? (
                  <li className="nav-item dropdown">
                    <NavLink
                        className="nav-link dropdown-toggle"
                        to="#"
                        id="adminDropdown"
                        role="button"
                        data-bs-toggle="dropdown"
                        aria-expanded="false"
                    >
                      Admin Panel
                    </NavLink>
                    <ul className="dropdown-menu" aria-labelledby="adminDropdown">
                      <li
                          style={{ cursor: "pointer" }}
                          className="dropdown-item"
                          onClick={() => navigate("menu-item/menu-item-list")}
                      >
                        Menu Item
                      </li>
                      <li
                          style={{ cursor: "pointer" }}
                          className="dropdown-item"
                          onClick={() => navigate("order/my-orders")}
                      >
                        My Orders
                      </li>
                      <li
                          style={{ cursor: "pointer" }}
                          className="dropdown-item"
                          onClick={() => navigate("order/all-orders")}
                      >
                        All Orders
                      </li>
                    </ul>
                  </li>
              ) : (
                  <li className="nav-item">
                    <NavLink className="nav-link" to="/order/my-orders">
                      Orders
                    </NavLink>
                  </li>
              )}
              <li className="nav-item">
                <NavLink className="nav-link" to="/shopping-cart">
                  <i className="bi bi-cart"></i>{" "}
                  {userData.id && `(${shoppingCartFromStore.length})`}
                </NavLink>
              </li>
            </ul>
            <ul className="navbar-nav ms-auto mb-2 mb-lg-0">
              {userData.id ? (
                  <>
                    <li className="nav-item">
                  <span className="nav-link active">
                    Welcome, {userData.fullName}
                  </span>
                    </li>
                    <li className="nav-item">
                      <button
                          className="btn btn-outline-light rounded-pill mx-2"
                          style={{
                            height: "40px",
                            width: "100px",
                          }}
                          onClick={handleLogout}
                      >
                        Logout
                      </button>
                    </li>
                  </>
              ) : (
                  <>
                    <li className="nav-item">
                      <NavLink className="nav-link" to="/register">
                        Register
                      </NavLink>
                    </li>
                    <li className="nav-item">
                      <NavLink
                          className="btn btn-success rounded-pill text-white mx-2"
                          style={{
                            height: "40px",
                            width: "100px",
                          }}
                          to="/login"
                      >
                        Login
                      </NavLink>
                    </li>
                  </>
              )}
            </ul>
          </div>
        </div>
      </nav>
  );
}

export default Header;
