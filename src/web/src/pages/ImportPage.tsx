import { importHackerOne } from '../api';
import { useState } from 'react';

export default function ImportPage() {
  const [result, setResult] = useState<number | null>(null);
  const [loading, setLoading] = useState(false);

  async function doImport() {
    setLoading(true);
    try {
      const r = await importHackerOne();
      setResult(r.imported);
    } finally {
      setLoading(false);
    }
  }

  return (
    <div className="p-4 space-y-2">
      <button onClick={doImport} className="px-4 py-2 bg-green-600 text-white rounded">
        {loading ? 'Importing...' : 'Import HackerOne'}
      </button>
      {result !== null && <p>Imported {result} programs.</p>}
    </div>
  );
}
