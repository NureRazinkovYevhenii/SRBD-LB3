import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import './css/FilmDetails.css';

const FilmDetails = () => {
    const { id } = useParams();
    const [film, setFilm] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchFilmDetails = async () => {
            try {
                const response = await fetch(`https://localhost:7005/api/Movies/${id}`);
                const data = await response.json();
                setFilm(data);
            } catch (error) {
                console.error("Error fetching film details:", error);
            } finally {
                setLoading(false);
            }
        };

        fetchFilmDetails();
    }, [id]);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (!film) {
        return <div>Film not found</div>;
    }

    return (
        <div className="film-details-container">
            <div className="film-desc">
                <div className="film-image">
                    <img src={film.imageUrl} alt={film.name} />
                </div>
                <div className="film-info">
                    <h1>{film.name}</h1>
                    <p><strong>Release Year:</strong> {film.releaseYear}</p>
                    <p><strong>Rating:</strong> {film.rating} <span style={{ color: 'yellow' }}>⭐</span></p>
                    <p><strong>Country:</strong> {film.country}</p>
                    <p><strong>Author:</strong> {film.authorName}</p>
                    <p><strong>Company:</strong> {film.companyName}</p>
                    <p><strong>Description:</strong> {film.description}</p>
                    <p><strong>Price:</strong> ${film.price}</p>
                </div>
            </div>

            <div className="film-screenings">
                <h2>Screenings</h2>
                {film.screenings && film.screenings.length > 0 ? (
                    <div className="screenings-list">
                        {film.screenings.map((screening, index) => (
                            <div key={index} className="screening-item">
                                <p className="screening-time">
                                    {new Date(screening.screeningDate).toLocaleString('en-GB', {
                                        day: '2-digit',
                                        month: '2-digit',
                                        year: 'numeric',
                                        hour: '2-digit',
                                        minute: '2-digit',
                                        hour12: false,
                                    }).replace(/\//g, '.') }
                                </p>
                                <p>{screening.location}</p>
                            </div>
                        ))}
                    </div>
                ) : (
                    <p>No screenings available.</p>
                )}
            </div>
        </div>
    );
};

export default FilmDetails;
