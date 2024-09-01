import React from "react";
import {
  useDeleteMenuItemMutation,
  useGetMenuItemsQuery,
} from "../../Apis/menuItemApi";
import { toast } from "react-toastify";
import { MainLoader } from "../../Components/Page/Common";
import { menuItemModel } from "../../Interfaces";
import { useNavigate } from "react-router";

function MenuItemList() {
  const [deleteMenuItem] = useDeleteMenuItemMutation();
  const { data, isLoading } = useGetMenuItemsQuery(null);
  const navigate = useNavigate();

  const handleMenuItemDelete = async (id: number) => {
    toast.promise(
        deleteMenuItem(id),
        {
          pending: "Processing your request...",
          success: "Menu Item Deleted Successfully",
          error: "Error encountered",
        },
        {
          theme: "dark",
        }
    );
  };

  return (
      <>
        {isLoading && <MainLoader />}
        {!isLoading && (
            <div className="menu-item-list-container mx-5 mt-5">
              <h1 className="text-success mb-4">Menu Item List</h1>

              {data.result?.length > 0 ?
                  <table className="table table-striped table-hover align-middle">
                    <thead className="table-dark">
                    <tr>
                      <th scope="col">ID</th>
                      <th scope="col">Image</th>
                      <th scope="col">Name</th>
                      <th scope="col">Category</th>
                      <th scope="col">Price</th>
                      <th scope="col">Special Tag</th>
                      <th scope="col">Action</th>
                    </tr>
                    </thead>
                    <tbody>
                    {data.result.map((menuItem: menuItemModel) => (
                        <tr key={menuItem.id}>
                          <td>{menuItem.id}</td>
                          <td>
                            <img
                                src={menuItem.image}
                                alt="no content"
                                style={{width: "100%", maxWidth: "80px"}}
                                className="img-thumbnail"
                            />
                          </td>
                          <td>{menuItem.name}</td>
                          <td>{menuItem.category}</td>
                          <td>${menuItem.price.toFixed(2)}</td>
                          <td>{menuItem.specialTag || "None"}</td>
                          <td>
                            <button
                                className="btn btn-outline-info btn-sm me-2"
                                onClick={() =>
                                    navigate("/menuitem/menu-item-upsert/" + menuItem.id)
                                }
                            >
                              <i className="bi bi-pencil-fill"></i> Edit
                            </button>
                            <button
                                className="btn btn-outline-danger btn-sm"
                                onClick={() => handleMenuItemDelete(menuItem.id)}
                            >
                              <i className="bi bi-trash-fill"></i> Delete
                            </button>
                          </td>
                        </tr>
                    ))}
                    </tbody>
                  </table> : <p>There are currently no menu items to display</p>
              }

            </div>
        )}
      </>
  );
}

export default MenuItemList;
