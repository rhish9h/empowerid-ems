import { render } from '@testing-library/react';
import MyNavbar from './MyNavbar';

describe('MyNavbar Component', () => {
  it('renders correctly', () => {
    const { getByText } = render(<MyNavbar />);
    const brandElement = getByText('Rhish');
    expect(brandElement).toBeInTheDocument();
  });

  it('navigates to LinkedIn profile', () => {
    const { getByText } = render(<MyNavbar />);
    const linkedInLink = getByText('LinkedIn');
    expect(linkedInLink).toHaveAttribute('href', 'https://www.linkedin.com/in/rhishabh-hattarki/');
  });

  it('navigates to GitHub profile', () => {
    const { getByText } = render(<MyNavbar />);
    const gitHubLink = getByText('GitHub');
    expect(gitHubLink).toHaveAttribute('href', 'https://github.com/rhish9h');
  });

  it('navigates to Portfolio', () => {
    const { getByText } = render(<MyNavbar />);
    const portfolioLink = getByText('Portfolio');
    expect(portfolioLink).toHaveAttribute('href', 'https://rhish.in');
  });
});
