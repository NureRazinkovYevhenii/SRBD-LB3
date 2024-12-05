import React, { useState } from 'react';
import './css/TableFunction.css';

const FilmsByAuthorPriceCountry = () => {
    const [price, setPrice] = useState('');
    const [country, setCountry] = useState('');
    const [films, setFilms] = useState([]);
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setMessage('');
        setFilms([]);

        try {
            const response = await fetch(`https://localhost:7005/api/Transact/table-function?price=${price}&country=${country}`);
            const data = await response.json();

            if (response.ok) {
                setFilms(data);
            } else {
                setMessage(data.message || 'An error occurred.');
            }
        } catch (error) {
            setMessage('No films with this parameters');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="films-by-author-price-country-container">
            <h2>Films by Price, and Country</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="price">Price:</label>
                    <input
                        type="number"
                        id="price"
                        value={price}
                        onChange={(e) => setPrice(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="country">Country:</label>
                    <input
                        type="text"
                        id="country"
                        value={country}
                        onChange={(e) => setCountry(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" disabled={loading}>
                    {loading ? 'Loading...' : 'Get Films'}
                </button>
            </form>

            {message && <p>{message}</p>}

            {films.length > 0 && (
                <table>
                    <thead>
                        <tr>
                            <th>Film ID</th>
                            <th>Name</th>
                            <th>Description</th>
                            <th>Price</th>
                            <th>Author</th>
                        </tr>
                    </thead>
                    <tbody>
                        {films.map((film, index) => (
                            <tr key={index}>
                                <td>{film.FilmID}</td>
                                <td>{film.Name}</td>
                                <td>{film.Description}</td>
                                <td>{film.Price}</td>
                                <td>{film.Author}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    );
};

export default FilmsByAuthorPriceCountry;
