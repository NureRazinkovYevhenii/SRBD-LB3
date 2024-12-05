import React, { useState, useEffect } from "react";
import FilmCard from "../elements/FilmCard";
import "./css/Home.css";

const Home = () => {
    const [films, setFilms] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchFilms = async () => {
            try {
                const response = await fetch("https://localhost:7005/api/movies/films");
                if (!response.ok) throw new Error("Ошибка загрузки данных");
                const data = await response.json();
                setFilms(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchFilms();
    }, []);

    if (loading) return <div>Загрузка...</div>;
    if (error) return <div>{error}</div>;

    return (
        <div className="film-list">
            {films.map((film, index) => (
                <FilmCard
                    key={index}
                    id={film.id}
                    name={film.name}
                    imageUrl={film.imageUrl}
                    releaseYear={film.releaseYear}
                    rating={film.rating}
                />
            ))}
        </div>
    );
};

export default Home;
