import React, { useState, useEffect } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { Headphones, User, BarChart3, Menu, X } from 'lucide-react';

const Header: React.FC = () => {
  const [isScrolled, setIsScrolled] = useState(false);
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const location = useLocation();

  useEffect(() => {
    const handleScroll = () => {
      setIsScrolled(window.scrollY > 10);
    };

    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  const toggleMobileMenu = () => {
    setIsMobileMenuOpen(!isMobileMenuOpen);
  };

  const isActive = (path: string) => {
    return location.pathname === path;
  };

  return (
    <header 
      className={`fixed top-0 left-0 right-0 z-50 transition-all duration-300 ${
        isScrolled ? 'bg-white shadow-md' : 'bg-transparent'
      }`}
    >
      <div className="container mx-auto px-4 py-4">
        <div className="flex items-center justify-between">
          <Link to="/" className="flex items-center space-x-2">
            <Headphones size={28} className="text-indigo-900" />
            <span className="text-xl font-bold text-indigo-900">EarJourney</span>
          </Link>

          {/* Desktop Navigation */}
          <nav className="hidden md:flex items-center space-x-8">
            <Link 
              to="/" 
              className={`font-medium transition-colors duration-200 ${
                isActive('/') ? 'text-indigo-900' : 'text-gray-600 hover:text-indigo-900'
              }`}
            >
              Home
            </Link>
            <Link 
              to="/journey" 
              className={`font-medium transition-colors duration-200 ${
                isActive('/journey') ? 'text-indigo-900' : 'text-gray-600 hover:text-indigo-900'
              }`}
            >
              My Journey
            </Link>
            <Link 
              to="/compare" 
              className={`font-medium transition-colors duration-200 ${
                isActive('/compare') ? 'text-indigo-900' : 'text-gray-600 hover:text-indigo-900'
              }`}
            >
              Compare
            </Link>
            <Link 
              to="/reviews" 
              className={`font-medium transition-colors duration-200 ${
                isActive('/reviews') ? 'text-indigo-900' : 'text-gray-600 hover:text-indigo-900'
              }`}
            >
            Reviews
            </Link>
            <Link 
              to="/login" 
              className="ml-4 p-2 rounded-full bg-gray-100 hover:bg-gray-200 transition-colors duration-200"
            >
              <User size={20} className="text-gray-700" />
            </Link>
          </nav>

          {/* Mobile Menu Button */}
          <button 
            className="md:hidden p-2"
            onClick={toggleMobileMenu}
            aria-label="Toggle menu"
          >
            {isMobileMenuOpen ? (
              <X size={24} className="text-gray-800" />
            ) : (
              <Menu size={24} className="text-gray-800" />
            )}
          </button>
        </div>
      </div>

      {/* Mobile Navigation */}
      {isMobileMenuOpen && (
        <div className="md:hidden bg-white border-t py-4 px-4 shadow-lg">
          <nav className="flex flex-col space-y-4">
            <Link 
              to="/" 
              className={`px-4 py-2 rounded-md ${
                isActive('/') ? 'bg-indigo-50 text-indigo-900' : 'text-gray-700'
              }`}
              onClick={() => setIsMobileMenuOpen(false)}
            >
              Home
            </Link>
            <Link 
              to="/journey" 
              className={`px-4 py-2 rounded-md ${
                isActive('/journey') ? 'bg-indigo-50 text-indigo-900' : 'text-gray-700'
              }`}
              onClick={() => setIsMobileMenuOpen(false)}
            >
              My Journey
            </Link>
            <Link 
              to="/compare" 
              className={`px-4 py-2 rounded-md ${
                isActive('/compare') ? 'bg-indigo-50 text-indigo-900' : 'text-gray-700'
              }`}
              onClick={() => setIsMobileMenuOpen(false)}
            >
              Compare
            </Link>
            <Link 
              to="/reviews" 
              className={`px-4 py-2 rounded-md ${
                isActive('/reviews') ? 'bg-indigo-50 text-indigo-900' : 'text-gray-700'
              }`}
              onClick={() => setIsMobileMenuOpen(false)}
            >
              Review
            </Link>
            <Link 
              to="/login" 
              className="flex items-center space-x-2 px-4 py-2 rounded-md text-gray-700"
              onClick={() => setIsMobileMenuOpen(false)}
            >
              <User size={20} />
              <span>Profile</span>
            </Link>
          </nav>
        </div>
      )}
    </header>
  );
};

export default Header;