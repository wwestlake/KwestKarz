export default function Banner({ title, subtitle, cta, onClick }) {
    return (
      <section className="bg-background text-text py-16 px-4 text-center">
        <h2 className="text-4xl font-bold mb-4">{title}</h2>
        {subtitle && <p className="text-lg mb-6 max-w-xl mx-auto">{subtitle}</p>}
        {cta && (
          <button
            onClick={onClick}
            className="bg-accent text-white px-6 py-3 rounded hover:opacity-90"
          >
            {cta}
          </button>
        )}
      </section>
    );
  }
  