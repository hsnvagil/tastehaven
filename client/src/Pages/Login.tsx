import React, { useState } from "react";
import { useLoginUserMutation } from "../Apis/authApi";
import { inputHelper } from "../Helper";
import { apiResponse, userModel } from "../Interfaces";
import {jwtDecode} from "jwt-decode";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { setLoggedInUser } from "../Storage/Redux/userAuthSlice";
import { MainLoader } from "../Components/Page/Common";

function Login() {
  const [error, setError] = useState("");
  const [loginUser] = useLoginUserMutation();
  const [loading, setLoading] = useState(false);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [userInput, setUserInput] = useState({
    userName: "",
    password: "",
  });

  const handleUserInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    const tempData = inputHelper(e, userInput);
    setUserInput(tempData);
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoading(true);
    const response: apiResponse = await loginUser({
      userName: userInput.userName,
      password: userInput.password,
    });
    if (response.data) {
      const { token } = response.data.result;
      const { fullName, id, email, role }: userModel = jwtDecode(token);
      localStorage.setItem("token", token);
      dispatch(setLoggedInUser({ fullName, id, email, role }));
      navigate("/");
    } else if (response.error) {
      setError(response.error.data.message);
    }

    setLoading(false);
  };

  return (
      <div className="d-flex justify-content-center align-items-center min-vh-100">
        {loading && <MainLoader />}
        <div className="card shadow-lg p-4" style={{ maxWidth: "500px", width: "100%" }}>
          <form method="post" onSubmit={handleSubmit}>
            <h2 className="text-center mb-4">Login</h2>
            <div className="form-group mb-3">
              <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Username"
                  required
                  name="userName"
                  value={userInput.userName}
                  onChange={handleUserInput}
              />
            </div>
            <div className="form-group mb-3">
              <input
                  type="password"
                  className="form-control"
                  placeholder="Enter Password"
                  required
                  name="password"
                  value={userInput.password}
                  onChange={handleUserInput}
              />
            </div>
            <div className="mt-2">
              {error && <p className="text-danger text-center">{error}</p>}
              <div className="d-grid">
                <button
                    type="submit"
                    className="btn btn-success btn-block"
                    disabled={loading}
                >
                  Login
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
  );
}

export default Login;
