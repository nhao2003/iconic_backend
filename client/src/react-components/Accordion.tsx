import React, { useState } from 'react';

interface Faq {
  _id: string;
  category_id: string;
  question: string;
  answer: string;
  createdAt: string;
  updatedAt: string;
  score: number;
}

interface FaqAccordionProps {
  faqs: Faq[];
}

const FaqAccordion: React.FC<FaqAccordionProps> = ({ faqs }) => {
  const [expanded, setExpanded] = useState<string | null>(null);

  const handleToggle = (id: string) => {
    setExpanded(expanded === id ? null : id);
  };

  const accordionContainerStyle = {
    fontFamily: 'Arial, sans-serif',
    fontSize: '12px',
    color: '#333',
    margin: '0 auto',
    width: '80%',
    maxWidth: '800px',
    padding: '16px',
    alignSelf: 'left',
  };

  const accordionItemStyle = {
    border: '1px solid #ccc',
    borderRadius: '8px',
    marginBottom: '12px',
    boxShadow: '0 2px 5px rgba(0, 0, 0, 0.1)',
  };

  const accordionHeaderStyle = {
    padding: '8px 0 0 16px',
    fontSize: '12px',  // Cỡ chữ câu hỏi
    backgroundColor: '#f5f5f5',
    cursor: 'pointer',
    fontWeight: 'bold',
    borderBottom: '1px solid #ccc',
    transition: 'background-color 0.3s ease',
  };

  const accordionBodyStyle = {
    padding: '10px 16px',
    backgroundColor: '#f9f9f9',
    fontSize: '12px',
  };

  const accordionHeaderHoverStyle = {
    backgroundColor: '#e0e0e0',
  };

  return (
    <div style={accordionContainerStyle}>
      {faqs.map((faq) => (
        <div key={faq._id} style={accordionItemStyle}>
          <div
            onClick={() => handleToggle(faq._id)}
            style={accordionHeaderStyle}
            onMouseOver={(e: React.MouseEvent) => ((e.currentTarget as HTMLElement).style.backgroundColor = accordionHeaderHoverStyle.backgroundColor)}
            onMouseOut={(e: React.MouseEvent) => ((e.currentTarget as HTMLElement).style.backgroundColor = '#f5f5f5')}
          >
            {faq.question}
          </div>
          {expanded === faq._id && (
            <div style={accordionBodyStyle}>
              <p>{faq.answer}</p>
            </div>
          )}
        </div>
      ))}
    </div>
  );
};

export default FaqAccordion;
