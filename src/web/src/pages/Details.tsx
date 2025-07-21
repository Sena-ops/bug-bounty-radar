import { useQuery } from '@tanstack/react-query';
import { fetchProgram } from '../api';
import { useParams, Link } from 'react-router-dom';

export default function Details() {
  const { id } = useParams();
  const { data, isLoading, error } = useQuery({
    queryKey: ['program', id],
    queryFn: () => fetchProgram(id!),
    enabled: !!id,
  });

  if (isLoading) return <p>Loading...</p>;
  if (error || !data) return <p>Error</p>;

  return (
    <div className="p-4">
      <Link to="/" className="text-blue-500">Back</Link>
      <h1 className="text-xl font-bold mt-2">{data.name}</h1>
      <p>ID: {data.id}</p>
    </div>
  );
}
