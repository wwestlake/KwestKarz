import Header from "./Header";

export default function Layout({ children }) {
  return (
    <div className="min-h-screen flex flex-col bg-background text-text overflow-x-hidden">
      <Header />
      <main className="flex-1 px-4 py-6">{children}</main>
      <footer className="bg-secondary text-sm text-text text-center py-4">
        &copy; {new Date().getFullYear()} Kwest Karz. All rights reserved.
      </footer>
    </div>
  );
}
// This is the main layout component that wraps around all pages.
// It includes the Header at the top, a main content area, and a footer.