import React from 'react';
import "bootstrap/dist/css/bootstrap.css"
import "bootstrap/dist/js/bootstrap.js"
import "bootstrap-icons/font/bootstrap-icons.json"
import {Header} from '../Components/Layout';
import {Home, NotFound, MenuItemDetails} from '../Pages';
import {Routes, Route} from "react-router-dom"

function App() {
    return (
        <div>
            <Header/>
            <div className='pb-5'>
                <Routes>
                    <Route path='/' element={<Home/>}></Route>
                    <Route path='*' element={<NotFound/>}></Route>
                    <Route path="/menuItemDetails/:menuItemId" element={<MenuItemDetails/>} ></Route>
                </Routes>
            </div>
        </div>
    );
}

export default App;
