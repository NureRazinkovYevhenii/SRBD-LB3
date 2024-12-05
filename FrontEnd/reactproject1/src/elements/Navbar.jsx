import React from "react";
import { Link } from "react-router-dom";
import './css/Navbar.css';

const Navbar = () => {
    return (
        <nav className="navbar">
            <ul className="navbar-list">
                <li>
                    <Link to="/">Home</Link>
                </li>
                <li>
                    <Link to="/procedure">Procedure</Link>
                </li>
                <li>
                    <Link to="/function">Scalar Function</Link>
                </li>
                <li>
                    <Link to="/table-function">Table Function</Link>
                </li>
            </ul>
        </nav>
    );
};

export default Navbar;
