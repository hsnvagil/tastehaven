import React from 'react'
import {menuItemModel} from '../../../Interfaces';
import {Link} from 'react-router-dom';

interface Props {
    menuItem: menuItemModel
}


function MenuItemCard(props: Props) {
    return (
        <div className='col-md-4 col-12 p-4'>
            <div className="card h-100 d-flex flex-column">
                <div className="badge bg-success text-white position-absolute top-0 end-0 m-2">
                    {props.menuItem.specialTag}
                </div>


                <Link to={`/menuItemDetails/${props.menuItem.id}`}>
                    <img src={props.menuItem.image} className="card-img-top" alt={props.menuItem.name}
                         style={{height: '150px', objectFit: 'cover'}}/>
                </Link>


            <div className="card-body d-flex flex-column">
                <Link className={"text-decoration-none text-dark"} to={`/menuItemDetails/${props.menuItem.id}`}>
                    <h5 className="card-title">{props.menuItem.name}</h5>
                </Link>

                <h6 className="card-subtitle mb-2 text-muted">{props.menuItem.category}</h6>

                    <p className="card-text flex-grow-1">{props.menuItem.description}</p>

                    <p className="card-text">
                        <strong>Price: ${props.menuItem.price.toFixed(2)}</strong>
                    </p>

                    <button className="btn btn-primary mt-auto">
                        Add to Basket
                    </button>
                </div>
            </div>
        </div>
    )
}

export default MenuItemCard;