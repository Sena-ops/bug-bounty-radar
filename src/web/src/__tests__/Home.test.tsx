import { render, screen } from '@testing-library/react';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import Home from '../pages/Home';

test('renders search input', () => {
  const qc = new QueryClient();
  render(
    <QueryClientProvider client={qc}>
      <Home />
    </QueryClientProvider>
  );
  expect(screen.getByPlaceholderText(/search/i)).toBeInTheDocument();
});
