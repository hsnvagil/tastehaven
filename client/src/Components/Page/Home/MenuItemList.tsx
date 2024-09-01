import React, { useState, useEffect } from "react";
import { useGetMenuItemsQuery } from "../../../Apis/menuItemApi";
import { menuItemModel } from "../../../Interfaces";
import MenuItemCard from "./MenuItemCard";
import { useDispatch, useSelector } from "react-redux";
import { setMenuItem, setSearchItem } from "../../../Storage/Redux/menuItemSlice";
import { MainLoader } from "../Common";
import { RootState } from "../../../Storage/Redux/store";
import { SD_SortTypes } from "../../../Utility/SD";

function MenuItemList() {
    const [menuItems, setMenuItems] = useState<menuItemModel[]>([]);
    const [selectedCategory, setSelectedCategory] = useState("All");
    const [categoryList, setCategoryList] = useState([""]);
    const [value, setValue] = useState("");

    const dispatch = useDispatch();
    const [sortName, setSortName] = useState(SD_SortTypes.NAME_A_Z);
    const { data, isLoading } = useGetMenuItemsQuery(null);
    const sortOptions: Array<SD_SortTypes> = [
        SD_SortTypes.PRICE_LOW_HIGH,
        SD_SortTypes.PRICE_HIGH_LOW,
        SD_SortTypes.NAME_A_Z,
        SD_SortTypes.NAME_Z_A,
    ];
    const searchValue = useSelector(
        (state: RootState) => state.menuItemStore.search
    );

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        dispatch(setSearchItem(e.target.value));
        setValue(e.target.value);
    };

    useEffect(() => {
        if (data && data.result) {
            const tempMenuArray = handleFilters(
                sortName,
                selectedCategory,
                searchValue
            );
            setMenuItems(tempMenuArray);
        }
    }, [searchValue]);

    useEffect(() => {
        if (!isLoading) {
            dispatch(setMenuItem(data.result));
            setMenuItems(data.result);
            const tempCategoryList = ["All"];
            data.result.forEach((item: menuItemModel) => {
                if (!tempCategoryList.includes(item.category)) {
                    tempCategoryList.push(item.category);
                }
            });
            setCategoryList(tempCategoryList);
        }
    }, [isLoading]);

    const handleSortClick = (i: number) => {
        setSortName(sortOptions[i]);
        const tempArray = handleFilters(
            sortOptions[i],
            selectedCategory,
            searchValue
        );
        setMenuItems(tempArray);
    };

    const handleCategoryClick = (i: number) => {
        setSelectedCategory(categoryList[i]);
        const tempArray = handleFilters(
            sortName,
            categoryList[i],
            searchValue
        );
        setMenuItems(tempArray);
    };

    const handleFilters = (
        sortType: SD_SortTypes,
        category: string,
        search: string
    ) => {
        let tempArray =
            category === "All"
                ? [...data.result]
                : data.result.filter(
                    (item: menuItemModel) =>
                        item.category.toUpperCase() === category.toUpperCase()
                );

        if (search) {
            tempArray = tempArray.filter((item: menuItemModel) =>
                item.name.toUpperCase().includes(search.toUpperCase())
            );
        }

        switch (sortType) {
            case SD_SortTypes.PRICE_LOW_HIGH:
                tempArray.sort((a: menuItemModel, b: menuItemModel) => a.price - b.price);
                break;
            case SD_SortTypes.PRICE_HIGH_LOW:
                tempArray.sort((a: menuItemModel, b: menuItemModel) => b.price - a.price);
                break;
            case SD_SortTypes.NAME_A_Z:
                tempArray.sort((a: menuItemModel, b: menuItemModel) => a.name.localeCompare(b.name));
                break;
            case SD_SortTypes.NAME_Z_A:
                tempArray.sort((a: menuItemModel, b: menuItemModel) => b.name.localeCompare(a.name));
                break;
            default:
                break;
        }

        return tempArray;
    };

    if (isLoading) {
        return <MainLoader />;
    }

    return (
        <div className="container">
            <div className="d-flex justify-content-center align-items-center my-3">
                <input
                    type="text"
                    className="form-control"
                    style={{ maxWidth: "400px", padding: "10px" }}
                    value={value}
                    onChange={handleChange}
                    placeholder="Search for Food Items"
                />
            </div>

            <div className="d-flex flex-wrap justify-content-between align-items-center mb-3">
                <div className="btn-group">
                    {categoryList.map((categoryName, index) => (
                        <button
                            key={index}
                            className={`btn btn-outline-secondary ${selectedCategory === categoryName ? "active" : ""
                            }`}
                            onClick={() => handleCategoryClick(index)}
                        >
                            {categoryName}
                        </button>
                    ))}
                </div>
                <div className="dropdown">
                    <button
                        className="btn btn-outline-dark dropdown-toggle"
                        type="button"
                        id="dropdownMenuButton"
                        data-bs-toggle="dropdown"
                        aria-expanded="false"
                    >
                        {sortName}
                    </button>
                    <ul className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                        {sortOptions.map((sortType, index) => (
                            <li key={index}>
                                <button
                                    className="dropdown-item"
                                    onClick={() => handleSortClick(index)}
                                >
                                    {sortType}
                                </button>
                            </li>
                        ))}
                    </ul>
                </div>
            </div>

            <div className="row">
                {menuItems.length > 0 ? (
                    menuItems.map((menuItem: menuItemModel, index: number) => (
                            <MenuItemCard menuItem={menuItem} />
                    ))
                ) : (
                    <div className="d-flex justify-content-center">
                        <p>No menu items available.</p>
                    </div>
                )}
            </div>
        </div>
    );
}

export default MenuItemList;
