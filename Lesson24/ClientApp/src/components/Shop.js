import React, { Component } from 'react';

export class Shop extends Component {
  static displayName = Shop.name;

  constructor(props) {
    super(props);
    this.state = { products: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderCatalog(products) {
    return (
        <div className="row">
            {products.map(product => {
              const imageUrl = `/api/image/${product.id}`;
              return (
              <div className="col-sm-4" key={product.id}>
                <div className="card m-3" style={{width: "18rem"}}>
                  <img className="card-img-top" src={imageUrl} alt="Card image cap"/>
                  <div className="card-body">
                    <h5 className="card-title">{product.name ?? "Some name"}</h5>
                    <p className="card-text">{product.description ?? "Some description"}</p>
                    <p className="card-text">Цена: {product.price ?? 0}</p>
                    <button className="btn btn-primary">В корзину</button>
                  </div>
                </div>
              </div>)}
            )}
        </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Shop.renderCatalog(this.state.products);

    return (
      <div>
        <h1 id="tabelLabel" >Shop catalog</h1>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('/api/product/list');
    const data = await response.json();
    this.setState({ products: data, loading: false });
  }
}
