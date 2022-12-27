import React from 'react';
import {Outlet} from "react-router-dom";
import Header from '../pages/common/Header';

const GeneralLayout = (props) => {
    return (
        <div className="flex">
            <Header />
            <Outlet />
        </div>
    )
}

export default GeneralLayout;