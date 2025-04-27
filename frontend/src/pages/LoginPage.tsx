import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const LoginPage: React.FC = () => {
  const [role, setRole] = useState('admin'); // Default role
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (role === 'admin') {
      // Admin Login
      if (email === 'admin@gmail.com' && password === 'admin') {
        navigate('/admin');
      } else {
        setError('Invalid Admin Credentials');
      }
    } else {
      // Customer Login API
      try {
        const response = await axios.post('http://localhost:5259/api/user/login', {
          email,
          password,
        });

        console.log('Login successful:', response.data);
        navigate('/customer'); // Customer Dashboard
      } catch (err) {
        console.error('Login failed:', err);
        setError('Invalid Customer Credentials');
      }
    }
  };

  const handleRegisterRedirect = () => {
    navigate('/register'); // 👈 Add this route
  };

  const handleForgotPassword = () => {
    alert('Forgot Password functionality coming soon!');
    // Or navigate('/forgot-password');
  };

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      <div className="w-full max-w-md p-8 bg-white rounded-lg shadow-lg">
        <h2 className="text-2xl font-bold text-center text-gray-800 mb-6">Login</h2>

        {/* Role Selection */}
        <div className="mb-4">
          <label htmlFor="role" className="block text-sm font-medium text-gray-700 mb-1">
            Select Role
          </label>
          <select
            id="role"
            value={role}
            onChange={(e) => setRole(e.target.value)}
            className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-400"
          >
            <option value="admin">Admin</option>
            <option value="customer">Customer</option>
          </select>
        </div>

        {/* Error Message */}
        {error && (
          <div className="mb-4 p-3 text-sm text-red-700 bg-red-100 border border-red-300 rounded">
            {error}
          </div>
        )}

        {/* Login Form */}
        <form onSubmit={handleLogin} className="space-y-4">
          <div>
            <label htmlFor="email" className="block text-sm font-medium text-gray-700 mb-1">
              Email
            </label>
            <input
              id="email"
              type="email"
              required
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-400"
              placeholder="example@gmail.com"
            />
          </div>

          <div>
            <label htmlFor="password" className="block text-sm font-medium text-gray-700 mb-1">
              Password
            </label>
            <input
              id="password"
              type="password"
              required
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-400"
              placeholder="••••••••"
            />
          </div>

          <button
            type="submit"
            className="w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition duration-200"
          >
            Login
          </button>
        </form>

        {/* Register & Forgot Password Links */}
        <div className="flex justify-between mt-4 text-sm">
          <button onClick={handleRegisterRedirect} className="text-blue-600 hover:underline">
            New Customer? Register
          </button>
          <button onClick={handleForgotPassword} className="text-blue-600 hover:underline">
            Forgot Password?
          </button>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
