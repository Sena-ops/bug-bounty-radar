export const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000';

export async function fetchPrograms(search = '', page = 1) {
  const params = new URLSearchParams({ search, page: page.toString() });
  const res = await fetch(`${API_URL}/programs?${params}`);
  if (!res.ok) throw new Error('Failed to load');
  return res.json();
}

export async function fetchProgram(id: string) {
  const res = await fetch(`${API_URL}/programs/${id}`);
  if (!res.ok) throw new Error('Not found');
  return res.json();
}

export async function importHackerOne() {
  const res = await fetch(`${API_URL}/import/hackerone`, { method: 'POST' });
  if (!res.ok) throw new Error('Import failed');
  return res.json();
}
