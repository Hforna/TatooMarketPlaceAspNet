﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TatooMarket.Exception.Exceptions {
    using System;
    
    
    /// <summary>
    ///   Uma classe de recurso de tipo de alta segurança, para pesquisar cadeias de caracteres localizadas etc.
    /// </summary>
    // Essa classe foi gerada automaticamente pela classe StronglyTypedResourceBuilder
    // através de uma ferramenta como ResGen ou Visual Studio.
    // Para adicionar ou remover um associado, edite o arquivo .ResX e execute ResGen novamente
    // com a opção /str, ou recrie o projeto do VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResourceExceptMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public ResourceExceptMessages() {
        }
        
        /// <summary>
        ///   Retorna a instância de ResourceManager armazenada em cache usada por essa classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TatooMarket.Exception.Exceptions.ResourceExceptMessages", typeof(ResourceExceptMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Substitui a propriedade CurrentUICulture do thread atual para todas as
        ///   pesquisas de recursos que usam essa classe de recurso de tipo de alta segurança.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a This e-mail already exists.
        /// </summary>
        public static string EMAIL_EXISTS {
            get {
                return ResourceManager.GetString("EMAIL_EXISTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Email format is wrong.
        /// </summary>
        public static string EMAIL_FORMAT {
            get {
                return ResourceManager.GetString("EMAIL_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a E-mail or password invalid.
        /// </summary>
        public static string EMAIL_PASSWORD_INVALID {
            get {
                return ResourceManager.GetString("EMAIL_PASSWORD_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a File format is wrong.
        /// </summary>
        public static string FILE_FORMAT {
            get {
                return ResourceManager.GetString("FILE_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a The authorization token is invaild or expired.
        /// </summary>
        public static string INVALID_TOKEN {
            get {
                return ResourceManager.GetString("INVALID_TOKEN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Password must have 8 or more digits.
        /// </summary>
        public static string PASSWORD_GREATER_OR_EQUAL_EIGHT {
            get {
                return ResourceManager.GetString("PASSWORD_GREATER_OR_EQUAL_EIGHT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a Password is not the same.
        /// </summary>
        public static string REPEAT_PASSWORD_ERROR {
            get {
                return ResourceManager.GetString("REPEAT_PASSWORD_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a This studio name already exists.
        /// </summary>
        public static string STUDIO_NAME_EXISTS {
            get {
                return ResourceManager.GetString("STUDIO_NAME_EXISTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a User already has a studio.
        /// </summary>
        public static string USER_ALREADY_HAS_STUDIO {
            get {
                return ResourceManager.GetString("USER_ALREADY_HAS_STUDIO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a User doens&apos;t exists
        ///.
        /// </summary>
        public static string USER_DOESNT_EXISTS {
            get {
                return ResourceManager.GetString("USER_DOESNT_EXISTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a This username already exists.
        /// </summary>
        public static string USERNAME_EXISTS {
            get {
                return ResourceManager.GetString("USERNAME_EXISTS", resourceCulture);
            }
        }
    }
}
