import React, { useState } from "react";
import { useRegisterUserMutation } from "../Apis/authApi";
import { inputHelper, toastNotify } from "../Helper";
import { apiResponse } from "../Interfaces";
import { SD_Roles } from "../Utility/SD";
import { useNavigate } from "react-router-dom";
import { MainLoader } from "../Components/Page/Common";

function Register() {
  const [registerUser] = useRegisterUserMutation();
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const [userInput, setUserInput] = useState({
    userName: "",
    password: "",
    role: "",
    name: "",
  });

  const handleUserInput = (
      e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const tempData = inputHelper(e, userInput);
    setUserInput(tempData);
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoading(true);
    const response: apiResponse = await registerUser({
      userName: userInput.userName,
      password: userInput.password,
      role: userInput.role,
      name: userInput.name,
    });
    if (response.data) {
      toastNotify("Registration successful! Please login to continue.");
      navigate("/login");
    } else if (response.error) {
      toastNotify(response.error.data.errorMessages[0], "error");
    }

    setLoading(false);
  };

  return (
      <div className="d-flex justify-content-center align-items-center min-vh-100">
        {loading && <MainLoader />}
        <div className="card shadow-lg p-4" style={{ maxWidth: "500px", width: "100%" }}>
          <form method="post" onSubmit={handleSubmit}>
            <h2 className="text-center mb-4">Register</h2>
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
                  type="text"
                  className="form-control"
                  placeholder="Enter Name"
                  required
                  name="name"
                  value={userInput.name}
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
            <div className="form-group mb-4">
              <select
                  className="form-control form-select"
                  required
                  value={userInput.role}
                  name="role"
                  onChange={handleUserInput}
              >
                <option value="">--Select Role--</option>
                <option value={`${SD_Roles.CUSTOMER}`}>Customer</option>
                <option value={`${SD_Roles.ADMIN}`}>Admin</option>
              </select>
            </div>
            <div className="d-grid">
              <button type="submit" className="btn btn-success btn-block" disabled={loading}>
                Register
              </button>
            </div>
          </form>
        </div>
      </div>
  );
}

export default Register;
