import { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { fetchPrograms } from '../api';
import { Link } from 'react-router-dom';

export default function Home() {
  const [search, setSearch] = useState('');
  const { data, isLoading, error, refetch } = useQuery({
    queryKey: ['programs', search],
    queryFn: () => fetchPrograms(search),
  });

  return (
    <div className="p-4 space-y-2">
      <h1 className="text-2xl font-bold">Programs</h1>
      <input
        className="border p-1"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        placeholder="search"
      />
      <button onClick={() => refetch()} className="ml-2 px-2 py-1 bg-blue-500 text-white rounded">
        Search
      </button>
      {isLoading && <p>Loading...</p>}
      {error && <p>Error</p>}
      <ul className="list-disc pl-5">
        {data?.map((p: any) => (
          <li key={p.id}>
            <Link to={`/programs/${p.id}`}>{p.name}</Link>
          </li>
        ))}
      </ul>
    </div>
  );
}
