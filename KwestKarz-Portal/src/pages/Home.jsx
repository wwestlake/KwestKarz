import Banner from "../components/Banner";

export default function Home() {
  return (
    <>
      <Banner
        title="Metro Detroit Car Rentals, Reinvented"
        subtitle="Well-maintained, reliable vehicles on your schedule – all through the Turo platform."
        cta="Browse Our Fleet"
        onClick={() => window.location.href = "/vehicles"}
      />
      <div className="text-center text-sm text-secondary mt-8">
        <p>More info and feature sections coming soon.</p>
      </div>
    </>
  );
}
