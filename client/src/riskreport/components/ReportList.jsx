import React, { useState, useEffect } from "react";
import reportService from "../services/ReportService";

function ReportList({ orgId }) {
  const [reports, setReports] = useState([]);
  const [loading, setLoading] = useState(false);

  const fetchReports = async () => {
    if (!orgId) {
      setReports([]);
      return;
    }

    setLoading(true);
    try {
      const res = await reportService.getReports(orgId);
      setReports(res.data || []);
    } catch (err) {
      console.error(err);
      setReports([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchReports();
  }, [orgId]);

  if (!orgId) {
    return (
      <div className="p-4 bg-white rounded shadow text-black text-center">
        Enter an Org ID to view reports.
      </div>
    );
  }

  if (loading) {
    return (
      <div className="p-4 bg-white rounded shadow text-black text-center">
        Loading reports...
      </div>
    );
  }

  if (reports.length === 0) {
    return (
      <div className="p-4 bg-white rounded shadow text-black text-center">
        No records found.
      </div>
    );
  }

  return (
    <div className="overflow-x-auto bg-white rounded shadow p-4 text-black">
      <table className="min-w-full border border-gray-200 rounded text-black">
        <thead className="bg-gray-100 text-black">
          <tr>
            <th className="py-2 px-4 border-b text-left">Report ID</th>
            <th className="py-2 px-4 border-b text-left">Period</th>
            <th className="py-2 px-4 border-b text-left">Metrics</th>
            <th className="py-2 px-4 border-b text-left">Created At</th>
          </tr>
        </thead>
        <tbody className="text-black">
          {reports.map((r) => {
            let metricsObj = {};
            try {
              metricsObj = JSON.parse(r.metrics);
            } catch (e) {
              console.error("Error parsing metrics", e);
            }

            return (
              <tr key={r.reportId} className="hover:bg-gray-50 text-black">
                <td className="py-2 px-4 border-b">{r.reportId}</td>
                <td className="py-2 px-4 border-b">{r.period}</td>
                <td className="py-2 px-4 border-b">
                  {Object.entries(metricsObj).map(([key, value]) => (
                    <div key={key} className="text-black">
                      <span className="font-medium">{key}:</span> {value}
                    </div>
                  ))}
                </td>
                <td className="py-2 px-4 border-b">{new Date(r.createdAt).toLocaleString()}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}

export default ReportList;
