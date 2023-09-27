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
        <div class="row">
            {products.map(product =>
              <div className="col-sm-4">
                <div className="card m-3" style={{width: "18rem"}}>
                  <img className="card-img-top" src="https://i.pickadummy.com/index.php?imgsize=600x400" alt="Card image cap"/>
                  <div className="card-body">
                    <h5 className="card-title">{product.Name ?? "Some name"}</h5>
                    <p className="card-text">{product.Description ?? "Some description"}</p>
                    <button className="btn btn-primary">В корзину</button>
                  </div>
                </div>
              </div>
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
    const response = await fetch('weatherforecast');
    const data = await response.json();
    this.setState({ products: data, loading: false });
  }
}
