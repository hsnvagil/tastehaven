import React, {useState} from "react";
import {Link, useParams} from "react-router-dom";
import {useGetMenuItemByIdQuery} from "../Apis/menuItemApi";


function MenuItemDetails() {
    const {menuItemId} = useParams();
    const {data, isLoading} = useGetMenuItemByIdQuery(menuItemId);
    const [quantity, setQuantity] = useState<number>(1);

    const handleQuantity = (counter: number) => {
        let newQuantity = quantity + counter;
        if (newQuantity === 0) {
            newQuantity = 1;
        }
        setQuantity(newQuantity);
        return;
    };

    return <div className="container my-5">
        {!isLoading ? (<>
            <div className="row">
                <div className="col-md-6">
                    <img
                        src={data.result?.image}
                        className="img-fluid" alt={'name'}
                        style={{height: '300px', objectFit: 'cover'}}/>
                </div>
                <div className="col-md-6">
                    <h1>{data.result?.name}</h1>
                    <h4 className="text-muted">{data.result?.category}</h4>
                    <p>{data.result?.description}</p>
                    <p><strong>Price: {data.result?.price}</strong></p>

                    <div className="d-flex align-items-center mb-3">
                        <button className="btn btn-outline-secondary" onClick={() => handleQuantity(-1)}>-</button>
                        <input type="number" className="form-control text-center mx-2" value={quantity} readOnly
                               style={{width: '60px'}}/>
                        <button className="btn btn-outline-secondary" onClick={() => handleQuantity(+1)}>+</button>
                    </div>

                    <button className="btn btn-primary me-2">Add to Basket</button>
                    <Link to="/" className="btn btn-secondary ">Back to Home</Link>

                    <div className="mt-3">
                    </div>
                </div>
            </div>
        </>) : (<div
            className={"d-flex justify-content-center"}
            style={{width: "100%"}}
        >
            <div>Loading...</div>
        </div>)}
    </div>
};

export default MenuItemDetails;