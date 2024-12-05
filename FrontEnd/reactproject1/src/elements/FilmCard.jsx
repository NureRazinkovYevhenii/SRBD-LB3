import React from "react";
import PropTypes from "prop-types";
import "./css/FilmCard.css";
import { Link } from "react-router-dom";

const FilmCard = ({ id, name, imageUrl, releaseYear, rating }) => {
    return (
        <Link to={`/films/${id}`} className="film-card" style={{ textDecoration: 'none', color: 'inherit' }}>
            <img src={imageUrl} alt={name} className="film-image" />
            <h3>{name}</h3>
            <p>{releaseYear} | {rating} <span style={{ color: 'yellow' }}>⭐</span></p>
        </Link>
    );
};

export default FilmCard;
