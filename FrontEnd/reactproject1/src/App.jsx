import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Home from "./pages/Home";
import FilmDetails from "./elements/FilmDetails";
import Navbar from "./elements/Navbar";
import Procedure from "./pages/Procedure";
import GetAuthorPriceCountryCount from './pages/ScalarFunction';
import FilmsByAuthorPriceCountry from './pages/TableFunction';

function App() {
    return (
        <Router>
            <Navbar />
                <Routes>
                    <Route path="/" element={<Home />} />
                <Route path="/films/:id" element={<FilmDetails />} />
                <Route path="/procedure" element={<Procedure />} />
                <Route path="/function" element={<GetAuthorPriceCountryCount />} />
                <Route path="/table-function" element={<FilmsByAuthorPriceCountry />} />
                </Routes>
        </Router>
    );
}

export default App;
