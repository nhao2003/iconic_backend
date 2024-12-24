import React from 'react';

interface Product {
  _id: string;
  original_id: number;
  name: string;
  description: string;
  price: number;
  score: number;
}

interface ProductListProps {
  products: Product[];
}

const ProductList: React.FC<ProductListProps> = ({ products }) => {
  // Các styles inline
  const productListStyle = {
    margin: '20px',
    fontFamily: "'Arial', sans-serif",
  };

  const productTitleStyle = {
    fontSize: '14px',
    color: '#333',
    marginBottom: '20px',
  };

  const productItemsStyle = {
    listStyleType: 'none',
    padding: 0,
    margin: 0,
  };

  const productItemStyle = {
    marginBottom: '12px',
    padding: '10px',
    backgroundColor: '#fff',
    border: '1px solid #ddd',
    borderRadius: '8px',
    transition: 'transform 0.3s ease, box-shadow 0.3s ease',
    boxShadow: '0 2px 4px rgba(0, 0, 0, 0.1)',
  };

  const productItemHoverStyle = {
    transform: 'translateY(-5px)', // Tạo hiệu ứng nâng lên
    boxShadow: '0 4px 8px rgba(0, 0, 0, 0.15)',
  };

  const productLinkStyle = {
    textDecoration: 'none',
    color: '#007bff',
    fontSize: '12px',
    fontWeight: 'bold',
    transition: 'color 0.3s ease',
  };

  const productLinkHoverStyle = {
    color: '#0056b3',
  };

  return (
    <div style={productListStyle}>
      <h2 style={productTitleStyle}>Danh sách sản phẩm</h2>
      <ul style={productItemsStyle}>
        {products.map((product) => (
          <li
            key={product._id}
            style={productItemStyle}
            onMouseOver={(e: React.MouseEvent) => {
              (e.currentTarget as HTMLElement).style.transform = productItemHoverStyle.transform;
              (e.currentTarget as HTMLElement).style.boxShadow = productItemHoverStyle.boxShadow;
            }}
            onMouseOut={(e: React.MouseEvent) => {
              (e.currentTarget as HTMLElement).style.transform = '';
              (e.currentTarget as HTMLElement).style.boxShadow = productItemStyle.boxShadow;
            }}
          >
            <a
              href={`/shop/${product.original_id}`}
              style={productLinkStyle}
              target="_blank"
              onMouseOver={(e: React.MouseEvent) => {
                ((e.currentTarget as HTMLElement) as HTMLElement).style.color = productLinkHoverStyle.color;
              }}
              onMouseOut={(e: React.MouseEvent) => {
                ((e.currentTarget as HTMLElement) as HTMLElement).style.color = productLinkStyle.color;
              }}
            >
              {product.name}
            </a>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ProductList;
