import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { BrowserRouter, Route, Routes, Link } from 'react-router-dom';
import Home from './pages/Home';
import Details from './pages/Details';
import ImportPage from './pages/ImportPage';
import './index.css';

const queryClient = new QueryClient();

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <nav className="p-2 bg-gray-800 text-white space-x-4">
          <Link to="/">Home</Link>
          <Link to="/import">Import</Link>
        </nav>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/programs/:id" element={<Details />} />
          <Route path="/import" element={<ImportPage />} />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}
