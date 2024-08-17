import React, {useState} from "react";
import {Link} from "react-router-dom";

function MenuItemDetails() {

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
        <div className="row">
            <div className="col-md-6">
                <img src={'https://tastehavenimages.blob.core.windows.net/tastehaven/merlin_200483841_0edb972a-b86a-4f14-8983-1e50e4977584-superJumbo.jpg'} className="img-fluid" alt={'name'}
                     style={{height: '300px', objectFit: 'cover'}}/>
            </div>
            <div className="col-md-6">
                <h1>{'Fried Calamari'}</h1>
                <h4 className="text-muted">{'Popular'}</h4>
                <p>{'Crispy fried calamari served with marinara sauce.'}</p>
                <p><strong>Price: ${12.99}</strong></p>

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
    </div>
};

export default MenuItemDetails;